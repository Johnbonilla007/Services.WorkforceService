using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Services.NetCore.Domain.Aggregates.Exceptions;
using Services.NetCore.Domain.Core;
using Services.NetCore.Infraestructure.Core;
using Services.NetCore.Infraestructure.Data.UnitOfWork;

namespace Services.NetCore.WebApi.Exceptions
{

    public class ExceptionMiddleware : IExceptionFilter
    {
        private readonly ILogger<ExceptionMiddleware> _logger;
        protected readonly IGenericRepository<IGenericDataContext> _repository;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, IGenericRepository<IGenericDataContext> repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public void OnException(ExceptionContext context)
        {
            // Log the exception
            _logger.LogError(context.Exception, "Unhandled exception occurred");

            var actionDescriptor = context.ActionDescriptor;
            string actionName = actionDescriptor.DisplayName;
            var controllerName = context.RouteData.Values["controller"]?.ToString();
            string httpMethod = context.HttpContext.Request.Method;

            // Retrieve query string parameters
            var queryParameters = context.HttpContext.Request.Query;
            Dictionary<string, string> requestParameters = new Dictionary<string, string>();
            DateTime creationDate = DateTime.Now;
            DateTime transactionDate = DateTime.Now;
            DateTime transactionDateUtc = DateTime.UtcNow;

            LogExceptions logExceptions = new LogExceptions
            {
                CreationDate = creationDate,
                TransactionDate = transactionDate,
                TransactionDateUtc = transactionDateUtc,
                ModifiedBy = queryParameters.FirstOrDefault(x => x.Key == "RequestUserInfo.modifiedBy").Value,
                TransactionType = Transactions.Added,
                TransactionDescription = Transactions.CreateLogException,
                Message = "An error occurred while processing your request",
                TransactionUId = Guid.NewGuid(),
                ExceptionError = context.Exception.Message.ToString(),
                ActionName = actionName,
                ControllerName = controllerName,
                HttpMethod = httpMethod,
                RequestParameters = queryParameters.Select(x => new RequestParameter
                {
                    Property = x.Key,
                    Value = x.Value,
                    ModifiedBy = queryParameters.FirstOrDefault(x => x.Key == "RequestUserInfo.modifiedBy").Value,
                    TransactionType = Transactions.Added,
                    TransactionDescription = Transactions.CreateLogException,
                    CreationDate = transactionDate,
                    TransactionDateUtc = transactionDateUtc,
                    TransactionDate = transactionDate,
                    TransactionUId = Guid.NewGuid(),
                }).ToList()
            };

            var response = Task.Run(async () =>
              {
                  await _repository.AddAsync(logExceptions);
                  await _repository.UnitOfWork.CommitAsync();
              });

            response.Wait();

            // Set the result to an error response
            context.Result = new ObjectResult(new ErrorResponse
            {
                Message = logExceptions.Message,
                ExceptionError = logExceptions.ExceptionError,
                ActionName = actionName,
                ControllerName = controllerName,
                Method = httpMethod
            })
            {
                StatusCode = (int)HttpStatusCode.InternalServerError
            };
        }
    }

    public class ErrorResponse
    {
        public string Message { get; set; }
        public string ExceptionError { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public string Method { get; set; }
    }
}
