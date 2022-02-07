using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoRumosWebApi.Dtos.Measurement
{
    public class GetMeasurementDto
    {
        public int Id { get; set; }
        public string MeasurementName { get; set; }
    }
}
