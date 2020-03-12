using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Layer
{
    public class EditVoziloViewModel
    {
        [Required]
        public int VehicleId { get; set; }

        [Required]
        public string ModelName { get; set; }

        [Required]
        public string LicensePlate { get; set; }

        [Required]
        public string VIN { get; set; }
    }
}
