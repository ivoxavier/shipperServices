using FastReport;
using FastReport.Export.PdfSimple;
using shipperApi.Models; // Adicione o using para os seus modelos
using System.IO;
using System.Threading.Tasks;

namespace shipperApi.Services;

public class FastReportGeneratorService : IReportGeneratorService
{
    public Task<byte[]> GeneratePdfAsync(string reportFileName, object data)
    {
        using var report = new Report();
        var reportPath = Path.Combine(AppContext.BaseDirectory, "Reports", reportFileName);

        if (!File.Exists(reportPath))
        {
            throw new FileNotFoundException("O ficheiro de relatório não foi encontrado.", reportPath);
        }

        report.Load(reportPath);

        // --- Passar dados para o relatório ---
        // Aqui, verificamos o tipo do objeto de dados e o registamos
        // para que o FastReport o possa usar.
        if (data is CreateExpeditionRequest expeditionData)
        {
            // O segundo parâmetro ("ExpeditionData") deve ser o mesmo nome
            // da fonte de dados que você configurou dentro do seu relatório .frx
            //report.RegisterData(new[] { expeditionData }, "ExpeditionData");

            // Exemplo de como passar parâmetros individuais
            //report.SetParameterValue("SenderNameParam", expeditionData.SenderName ?? "");
            //report.SetParameterValue("AddresseeAddressParam", expeditionData.AddresseeAddress ?? "");
        }
        
        //pode ter mais if

        report.Prepare();

        var pdfExport = new PDFSimpleExport();
        using var ms = new MemoryStream();
        report.Export(pdfExport, ms);

        return Task.FromResult(ms.ToArray());
    }
}