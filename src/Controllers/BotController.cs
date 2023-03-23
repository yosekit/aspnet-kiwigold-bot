using KiwigoldBot.Services;
using Microsoft.AspNetCore.Mvc;

using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace KiwigoldBot.Controllers
{
	[ApiController]
	[Route("api/telegrambot")]
	public class BotController : Controller
	{
		private readonly BotUpdateHandler _updateHandler;

        public BotController(BotUpdateHandler updateHandler)
        {
			_updateHandler = updateHandler;
        }

        [HttpPost]
		[Route("update")]
		public async Task<IActionResult> Update(
			[FromBody] Update update, CancellationToken cancellationToken)
		{
			if (update == null) { return Ok(); }

			await _updateHandler.HandleUpdateAsync(update, cancellationToken);
			return Ok();
		}

		[HttpGet]
		public string Index() => "The server is started";
	}
}
