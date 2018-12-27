using System.Collections.Generic;
using AspNetCore.Totp;
using AspNetCore.Totp.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Web2FA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ITotpGenerator _totpGenerator;
        private readonly ITotpSetupGenerator _totpSetupGenerator;
        private readonly ITotpValidator _totpValidator;


        public ValuesController(ITotpSetupGenerator totpSetupGenerator)
        {
            _totpSetupGenerator = totpSetupGenerator;
            _totpGenerator = new TotpGenerator();
            _totpValidator = new TotpValidator(_totpGenerator);
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new[] {"value1", "value2"};
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        // DELETE api/values/5

        [HttpGet]
        [Route("qr")]
        public ActionResult<IEnumerable<string>> Qr()
        {
            var totpSetup = _totpSetupGenerator.Generate("gzz-title", "gzz@gzz.cn", "secret");

            return new[] {totpSetup.ManualSetupKey, totpSetup.QrCodeImage};
        }

        [HttpGet]
        [Route("valid")]
        public ActionResult<IEnumerable<string>> Valid(int code)
        {
            var valid = _totpValidator.Validate("secret", code);

            return new[] {valid.ToString()};
        }
    }
}