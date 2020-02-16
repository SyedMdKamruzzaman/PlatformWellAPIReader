using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.DTO.ViewModel
{
    public class PlatformWellViewModel
    {
        public List<Platform> Platforms { get; set; }
        public List<Well> Wells{ get; set; }
        public PlatformWellActual PlatformWellActual { get; set; }
    }
}
