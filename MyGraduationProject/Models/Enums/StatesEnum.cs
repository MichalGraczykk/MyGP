using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyGraduationProject.Models.Enums
{
    public enum StatesEnum
    {
        //podajemy nazwe pod ktora chcemy zeby status byl dostepny w kontrlerach a po = podajemy jego id w bazie danych
        //uzywamy tego w porownaniach w nastepujacy sposob => STATE_ID == StatesEnum.AVAILABLE
        AVAILABLE = 1,
        UNAVAILABLE = 2
    }
}