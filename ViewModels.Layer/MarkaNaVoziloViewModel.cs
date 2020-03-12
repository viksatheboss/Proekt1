using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Layer
{
    public class MarkaNaVoziloViewModel
    {
        [Required]
        public int ModelId { get; set; }

        [Required]
        public string ModelName { get; set; }
    }
}
