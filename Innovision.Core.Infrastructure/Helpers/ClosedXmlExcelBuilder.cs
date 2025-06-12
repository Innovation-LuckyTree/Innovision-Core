using System.ComponentModel;
using System.Data;
using ClosedXML.Excel;

namespace ReportServices.Infrastructure.Helpers;

public class ClosedXmlExcelBuilder
{
    public static string ExcelContentType
    {
        get
        {
            return "application/vnd.openxmlformats-Officedocument.spreadsheetml.sheet";
        }
    }

    public static DataTable ListToDataTable<T>(List<T> data, Dictionary<string, string>? columnHeaders = null)
    {
        PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
        DataTable dataTable = new DataTable();

        foreach (PropertyDescriptor property in properties)
        {
            string columnName = columnHeaders?.ContainsKey(property.Name) == true
                ? columnHeaders[property.Name]
                : property.Name;

            dataTable.Columns.Add(columnName, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType);
        }

        object[] values = new object[properties.Count];
        foreach (T item in data)
        {
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = properties[i].GetValue(item);
            }
            dataTable.Rows.Add(values);
        }
        return dataTable;
    }

    public static XLWorkbook ExportExcel<T>(DataTable dataTable, Dictionary<string, string>? columnHeaders = null)
    {
        var workbook = new XLWorkbook();
        var ws = workbook.AddWorksheet("Sheet1");

        int columnIndex = 1;
        foreach (DataColumn column in dataTable.Columns)
        {
            ws.Cell(1, columnIndex).Style.Font.SetBold(true);
            ws.Cell(1, columnIndex).Value = column.ColumnName;
            columnIndex++;
        }

        ws.Cell(2, 1).InsertData(dataTable.AsEnumerable());

        ws.Columns().AdjustToContents();

        return workbook;
    }

    public static XLWorkbook ExportExcel<T>(List<T> data, Dictionary<string, string>? columnHeaders = null)
    {
        return ExportExcel<T>(ListToDataTable(data, columnHeaders), columnHeaders);
    }

    public static string ExportExcelToBase64<T>(List<T> data, Dictionary<string, string>? columnHeaders = null)
    {
        var workbook = ExportExcel<T>(ListToDataTable(data, columnHeaders), columnHeaders);

        using var ms = new MemoryStream();
        workbook.SaveAs(ms);

        return Convert.ToBase64String(ms.ToArray());
    }
}
