using AutoMapper;
using CsvHelper;
using student_management_sys.Configs;
using student_management_sys.Dto;
using student_management_sys.Entity;
using student_management_sys.Inputs;
using System.Formats.Asn1;
using System.Globalization;
using System.IO;

namespace student_management_sys.Services
{
    public class CSVService
    {
        public async Task<IEnumerable<StudentDto>> GetRecordsAsync(Stream file)   //Import From local
        {
            var reader = new StreamReader(file);
            var records = await Import(reader);

            return records;
        }

        private async Task<IEnumerable<StudentDto>> Import(StreamReader reader)
        {
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = csv.GetRecords<StudentDto>().ToList();

            return records;
        }

        
    }
}
