﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Param
{
    public class ParamPager : ParamJqGrid
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
    }
}
