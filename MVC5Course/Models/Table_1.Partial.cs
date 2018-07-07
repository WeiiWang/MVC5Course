namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(Table_1MetaData))]
    public partial class Table_1
    {
    }
    
    public partial class Table_1MetaData
    {
        [Required]
        public int test { get; set; }
        
        [StringLength(10, ErrorMessage="欄位長度不得大於 10 個字元")]
        public string aaa { get; set; }
    }
}
