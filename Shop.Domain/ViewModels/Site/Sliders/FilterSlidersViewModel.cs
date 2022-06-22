using Shop.Domain.Models.Site;
using Shop.Domain.ViewModels.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.ViewModels.Site.Sliders
{
    public class FilterSlidersViewModel : BasePaging
    {
        public string SliderTitle { get; set; }

        public List<Slider> Sliders { get; set; }

        #region metode
        public FilterSlidersViewModel SetSliders(List<Slider> sliders)
        {
            Sliders = sliders;
            return this;
        }


        public FilterSlidersViewModel SetPaging(BasePaging paging)
        {
            PageId = paging.PageId;
            AllEntityCount = paging.AllEntityCount;
            StartPage = paging.StartPage;
            EndPage = paging.EndPage;
            TakeEntity = paging.TakeEntity;
            CountForShowAfterAndBefor = paging.CountForShowAfterAndBefor;
            SkipEntitiy = paging.SkipEntitiy;
            PageCount = paging.PageCount;

            return this;
        }
        #endregion
    }
}
