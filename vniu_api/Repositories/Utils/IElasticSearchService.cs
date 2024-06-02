using Nest;

namespace vniu_api.Repositories.Utils
{
    public interface IElasticSearchService<T>
    {
        Task<string> CreateDocumentAsync(T document);
        Task<T> GetDocumentByIdAsync(string _idDocument);
        Task<IEnumerable<T>> GetAllDocuments();
        Task<string> UpdateDocumentAsync(T document);
        Task<string> DeleteDocumentByIdAsync(string _idDocument);
        Task<string> DeleteAllDocumentsAsync(string indexName);
        Task<IEnumerable<T>> QueryDocumentsAsync(int page, int pageSize);
    }
}
