using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

/*
 * Namspace: ~/Models/Project
 * Author: Namchok Singhachai
 * Description: Model for table dbo.Project.
 */

namespace UMS.Models
{
    public class Project
    {
        [Key]
        public int proj_Id { set; get; }
        [Column(TypeName = "nvarchar(256)")]
        public string proj_NameTH { set; get; }
        public string proj_NameEN { set; get; }
        [Column(TypeName = "nvarchar(256)")]
        public string proj_createDate { set; get; }
        public string proj_updateDate { set; get; }
        public string proj_stat_Id { set; get; }
        [NotMapped]
        public string proj_status { set; get; }
    } // End project
}
