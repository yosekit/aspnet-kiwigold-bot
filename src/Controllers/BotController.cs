using KiwigoldBot.Services;
using Microsoft.AspNetCore.Mvc;

using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace KiwigoldBot.Controllers
{
	public class BotController : ControllerBase
	{
		private readonly BotUpdateHandler _updateHandler;

        public BotController(BotUpdateHandler updateHandler)
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
