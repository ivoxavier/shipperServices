namespace shipperApi.Services;

public interface IReportGeneratorService
{

    Task<byte[]> GeneratePdfAsync(string reportFileName, object data);
}