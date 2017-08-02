using System.Collections.Generic;
using Algorate.Helpers;
using Algorate.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Algorate.Controllers
{
    [Route("api/[controller]")]
    public class CodeController : Controller
    {
        /// <summary>
        /// Returns the list of PENDING test ID's.
        /// </summary>
        /// <returns>A contact record with an HTTP 200, or null with an HTTP 404.</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<string>), 200)]
        public List<string> Get()
        {
            var codeEnum = CodeRunner.CodeQueue.GetEnumerator();
            var completed = new List<string>();
            while (codeEnum.MoveNext())
            {
                completed.Add(codeEnum.Current.CodeId);
            }
            codeEnum.Dispose();
            return completed;
        }

        /// <summary>
        /// Returns the test at the id if it is complete.
        /// </summary>
        /// <param name="id">The id of the CodeModel.</param>
        /// <returns>A contact record with an HTTP 200, or null with an HTTP 404.</returns>
        /// <response code="200">OK</response>
        /// <response code="404">Not Found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ResultModel), 200)]
        public ResultModel GetCode(string id)
        {
            ResultModel res;
            CodeRunner.CodeResults.Remove(id, out res);
            if (res != null)
            {
                return res;
            } else
            {
                return new ResultModel(false, -1, new List<string> { "Running code now." });
            }
        }

        /// <summary>
        /// Add a CodeModel to the queue. Returns a result message.
        /// </summary>
        /// <param name="cm">The CodeModel to add to the queue.</param>
        /// <returns>A contact record with an HTTP 200, or null with an HTTP 404.</returns>
        /// <response code="200">OK</response>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        public string Post([FromBody] CodeModel cm)
        {
            if (cm == null || !cm.Validate())
            {
                return "Could not validate code model.";
            }
            CodeRunner.AddCode(cm);

            ConsoleHelper.Success("Added code to queue: " + cm.CodeId);

            return "Added code to queue.";
        }
    }
}