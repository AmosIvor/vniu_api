using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using vniu_api.Exceptions;
using vniu_api.Repositories.Utils;
using vniu_api.ViewModels.ProfilesViewModels;

namespace vniu_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElasticSearchController : ControllerBase
    {
        private readonly IElasticSearchService<AddressVM> _elasticSearchService;

        public ElasticSearchController(IElasticSearchService<AddressVM> elasticSearchService)
        {
            _elasticSearchService = elasticSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDocuments()
        {
            var response = await _elasticSearchService.GetAllDocuments();

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDocument(AddressVM addressVM)
        {
            var result = await _elasticSearchService.CreateDocumentAsync(addressVM);

            return Ok(result);
        }

        [HttpGet("read/{idDocument}")]
        public async Task<IActionResult> GetDocumentById(string idDocument)
        {
            var document = await _elasticSearchService.GetDocumentByIdAsync(idDocument);

            if (document == null)
            {
                throw new NotFoundException("Document not found");
            }

            return Ok(document);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAllDocuments()
        {
            string indexName = "address";

            var response = await _elasticSearchService.DeleteAllDocumentsAsync(indexName);

            return Ok(response);
        }

        [HttpDelete("delete/{idDocument}")]
        public async Task<IActionResult> DeleteDocumentById(string idDocument)
        {
            var response = await _elasticSearchService.DeleteDocumentByIdAsync(idDocument);

            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDocument(AddressVM addressVM)
        {
            var response = await _elasticSearchService.UpdateDocumentAsync(addressVM);

            return Ok(response);
        }

        [HttpGet("query")]
        public async Task<IActionResult> QueryDocument(int page, int pageSize = 5)
        {
            var response = await _elasticSearchService.QueryDocumentsAsync(page, pageSize);

            return Ok(response);
        }
    }
}
