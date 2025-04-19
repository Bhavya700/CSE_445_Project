using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordFilterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WordFilterController : ControllerBase
    {
        //hardcoded words given by requirements that will be removed 
        private static readonly HashSet<string> StopWords = new HashSet<string> 
        {
            "a", "an", "in", "on", "the", "is", "are", "am", "of", "to", "and", "or", "but", "at", "by", "for", "with"
        };

        // POST api/wordfilter/filter
        [HttpPost("filter")]
        public ActionResult<string> FilterText([FromBody] WordFilterRequest request)
        {
            // Check if the request is null or empty
            if (request == null || string.IsNullOrWhiteSpace(request.Text))
                return BadRequest("Input text cannot be empty.");

            // Remove XML tags 
            string noXml = Regex.Replace(request.Text, "<.*?>", string.Empty);

            // Split the string into words and filter out stop words
            string[] words = Regex.Split(noXml, @"\W+");
            var filteredWords = words.Where(word => !StopWords.Contains(word.ToLower()) && word.Length > 0);

            // Join back the string of filtered words 
            return Ok(string.Join(" ", filteredWords));
        }
    }

    
    public class WordFilterRequest
    {
        public string Text { get; set; }
    }
}
