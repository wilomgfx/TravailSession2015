using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using GestionPhotoImmobilier.Models;

namespace GestionPhotoImmobilier.ViewModels
{
    public class AgentRapport
    {
        public Agent Agent { get; set; }
        public List<usp_ProduireRapportAgent_Result> RapportAgent { get; set; }
    }
}