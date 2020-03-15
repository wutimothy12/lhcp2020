using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lhcp2020.Models
{
    public class PaintingsListViewModel
    {
        public IEnumerable<ChinesePainting> CPaintings { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
