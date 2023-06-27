using Microsoft.AspNetCore.Mvc;

using Telegram.Bot.Types;

using KiwigoldBot.Interfaces;

namespace KiwigoldBot.Controllers
{
    public class BotController : ControllerBase
	{
		private readonly IBotUpdateHandler _updateHandler;

        public BotController(IBotUpdateHandler updateHandler)
        {
			_updateHandler = updateHandler;
        }

		[HttpGet]
		public IActionResult Index() => Ok("The server is started...");

        [HttpPost]
		public async Task<IActionResult> Update(
			[FromBody] Update update, CancellationToken cancellationToken)
		{
			if (update == null) { return Ok(); }

			await _updateHandler.HandleUpdateAsync(update, cancellationToken);
			return Ok();
		}
	}
}
