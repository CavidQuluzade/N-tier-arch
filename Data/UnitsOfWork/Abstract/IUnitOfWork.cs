﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.UnitsOfWork.Abstract
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
