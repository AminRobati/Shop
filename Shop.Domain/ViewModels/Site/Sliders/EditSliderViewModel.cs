using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Site.Sliders
{
    public  class EditSliderViewModel: CreateSliderViewModel
    {
        public long SliderId { get; set; }

        public string SliderImage { get; set; }
    }

    public enum EditSliderResult
    {
        NotFound,
        Success
    }
}
