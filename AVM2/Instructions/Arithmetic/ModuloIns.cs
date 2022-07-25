﻿namespace Flazzy.ABC.AVM2.Instructions
{
    public class ModuloIns : Computation
    {
        public ModuloIns()
            : base(OPCode.Modulo)
        { }

        protected override object Execute(object left, object right)
        {
            return Convert.ToDouble(left) % Convert.ToDouble(right);
        }
    }
}