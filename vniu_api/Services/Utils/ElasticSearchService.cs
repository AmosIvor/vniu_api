using Nest;
using vniu_api.Exceptions;
using vniu_api.Repositories.Utils;

namespace vniu_api.Services.Utils
{
    public class ElasticSearchService<T> : IElasticSearchService<T> where T : class
    {
        private readonly IElasticClient _elasticClient;
        public ElasticSearchService(IElasticClient elasticClient)
        {
            _elasticClient = elasticClient;
        }

        public async Task<string> CreateDocumentAsync(T document)
        {
            var response = await _elasticClient.IndexDocumentAsync(document);

            return response.IsValid ? "Document created successfully" : "Failed to create document";
        }

        public async Task<string> DeleteAllDocumentsAsync(string indexName)
        {
            var response = await _elasticClient.DeleteByQueryAsync<T>(d => d
                                                                .Index(indexName)
                                                                .Query(q => q.MatchAll())
                                                                );

            if (!(response.IsValid && response.Total > 0))
            {
                return "No documents found to delete";
            }

            return $"Delete {response.Total} documents";
        }

        public async Task<string> DeleteDocumentByIdAsync(string _idDocument)
        {
            var response = await _elasticClient.DeleteAsync(new DocumentPath<T>(_idDocument));

            return response.IsValid ? "Document deleted successfully" : "Failed to delete document";
        }

        public async Task<IEnumerable<T>> GetAllDocuments()
        {
            var searchResponse = await _elasticClient.SearchAsync<T>(s => s.MatchAll().Size(1000));

            return searchResponse.Documents;
        }

        public async Task<T> GetDocumentByIdAsync(string _idDocument)
        {
            var searchResponse = await _elasticClient.GetAsync(new DocumentPath<T>(_idDocument));

            return searchResponse.Source;
        }

        public async Task<IEnumerable<T>> QueryDocumentsAsync(int page, int pageSize)
        {
            int from = (page - 1) * pageSize;

            var searchResponse = await _elasticClient.SearchAsync<T>(s => s
               .MatchAll()
               .Sort(so => so.Ascending("addressId"))
               .From(from)
               .Size(pageSize)
            );

            return searchResponse.Documents;
        }

        public async Task<string> UpdateDocumentAsync(T document)
        {
            var response = await _elasticClient.UpdateAsync(new DocumentPath<T>(document), 
                                                    u => u.Doc(document).RetryOnConflict(3));

            return response.IsValid ? "Document updated successfully" : "Failed to update document";
        }
    }
}
