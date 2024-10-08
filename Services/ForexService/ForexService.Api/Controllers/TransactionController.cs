using System.Net;
using Microsoft.AspNetCore.Mvc;
using Framework.WebApi.Core.Controllers;
using Framework.Core.Mediator;
using NSwag.Annotations;
using ForexService.Application.Commands.Purchase;
using ForexService.Application.Commands.Sell;

namespace ForexService.Api.Controllers
{
    [ApiController]
    [Route("api/transaction/v1/")]
    [OpenApiTag("Activity workers", Description = "Activity workers services")]
    public class TransactionController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;
        public TransactionController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        /// <summary>
        /// Purchase a forex
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("purchase")]
        [ProducesResponseType(typeof(PurchaseCommandOutput),(int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ValidationProblemDetails),(int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails),(int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Purchase([FromBody] PurchaseCommand command)
        {
            var commandHandlerOutput = await _mediatorHandler.SendCommand<PurchaseCommand, PurchaseCommandOutput>(command);
            return CustomResponseStatusCodeAccepted(commandHandlerOutput, $"api/transaction/v1/{commandHandlerOutput.TransactionForexId}");
        }

          /// <summary>
        /// Sell a forex
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("sell")]
        [ProducesResponseType(typeof(PurchaseCommandOutput),(int)HttpStatusCode.Accepted)]
        [ProducesResponseType(typeof(ValidationProblemDetails),(int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails),(int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Sell([FromBody] SellCommand command)
        {
            var commandHandlerOutput = await _mediatorHandler.SendCommand<SellCommand, SellCommandOutput>(command);
            return CustomResponseStatusCodeAccepted(commandHandlerOutput, $"api/transaction/v1/{commandHandlerOutput.TransactionForexId}");
        }
    }
}
