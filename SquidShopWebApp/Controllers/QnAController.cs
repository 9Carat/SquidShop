using Azure.AI.Language.QuestionAnswering;
using Azure.AI.TextAnalytics;
using Azure;
using Microsoft.AspNetCore.Mvc;
using SquidShopWebApp.Models;
using SquidShopWebApp.Services.IServices;

namespace SquidShopWebApp.Controllers
{
    public class QnAController : Controller
    {
        private readonly ILogger<QnAController> _logger;
        private readonly IConfiguration _configuration;
        public QnAController(ILogger<QnAController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GetAnswer([FromBody] string mes)
        {
            string cogSvcEndpoint = _configuration["QnAEndpoint"];
            string cogSvcKey = _configuration["QnAKey"];
            string projectName = "SquidFaq";
            string deploymentName = "production";

            Uri endpoint = new Uri(cogSvcEndpoint);
            AzureKeyCredential credentials = new AzureKeyCredential(cogSvcKey);
            QuestionAnsweringClient client = new QuestionAnsweringClient(endpoint, credentials);
            QuestionAnsweringProject project = new QuestionAnsweringProject(projectName, deploymentName);
            
            Response<AnswersResult> response = client.GetAnswers(mes, project);
            if (response != null)
            {
                foreach (KnowledgeBaseAnswer answer in response.Value.Answers)
                {
                    return Json( answer );
                }
            }
            return View( response );
        }
    } 
}

