using OfficeOpenXml;
using Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BusinessLayer
{
    public class RelatorioBusinessLayer
    {
        public byte[] GerarExcelRelatorioCliente(IEnumerable<RelatorioCliente> relatorio)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Relatorio Cliente");
                worksheet.Cells[1, 1].Value = "Cliente";
                worksheet.Cells[1, 2].Value = "Total de Faturas";
                worksheet.Cells[1, 3].Value = "Total Valor";

                int row = 2;
                foreach (var item in relatorio)
                {
                    worksheet.Cells[row, 1].Value = item.Cliente;
                    worksheet.Cells[row, 2].Value = item.TotalFaturas;
                    worksheet.Cells[row, 3].Value = item.TotalValor;
                    row++;
                }

                return package.GetAsByteArray();
            }
        }

        public byte[] GerarExcelRelatorioAnoMes(IEnumerable<RelatorioAnoMes> relatorio)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Relatorio Ano-Mês");
                worksheet.Cells[1, 1].Value = "Ano";
                worksheet.Cells[1, 2].Value = "Mês";
                worksheet.Cells[1, 3].Value = "Total Faturas";
                worksheet.Cells[1, 4].Value = "Total Valor";

                int row = 2;
                foreach (var item in relatorio)
                {
                    worksheet.Cells[row, 1].Value = item.Ano;
                    worksheet.Cells[row, 2].Value = item.Mes;
                    worksheet.Cells[row, 3].Value = item.TotalFaturas;
                    worksheet.Cells[row, 4].Value = item.TotalValor;
                    row++;
                }

                return package.GetAsByteArray();
            }
        }

        public byte[] GerarExcelTop10Faturas(IEnumerable<Fatura> topFaturas)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Top 10 Faturas");
                worksheet.Cells[1, 1].Value = "Cliente";
                worksheet.Cells[1, 2].Value = "Data";
                worksheet.Cells[1, 3].Value = "Valor";

                int row = 2;
                foreach (var item in topFaturas)
                {
                    worksheet.Cells[row, 1].Value = item.Cliente;
                    worksheet.Cells[row, 2].Value = item.Data.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 3].Value = item.FaturaItem.Select(x=>x.Valor).Sum();
                    row++;
                }

                return package.GetAsByteArray();
            }
        }

        public byte[] GerarExcelTop10Itens(IEnumerable<FaturaItem> topItens)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Top 10 Itens");
                worksheet.Cells[1, 1].Value = "Descrição";
                worksheet.Cells[1, 2].Value = "Ordem";
                worksheet.Cells[1, 3].Value = "Valor";

                int row = 2;
                foreach (var item in topItens)
                {
                    worksheet.Cells[row, 1].Value = item.Descricao;
                    worksheet.Cells[row, 2].Value = item.Ordem;
                    worksheet.Cells[row, 3].Value = item.Valor;
                    row++;
                }

                return package.GetAsByteArray();
            }
        }

    }
}
