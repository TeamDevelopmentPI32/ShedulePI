//------------------------------------------------------------------------------
// <auto-generated>
//    Этот код был создан из шаблона.
//
//    Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//    Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmartScheduler.Models.DataContexts
{
    using System;
    using System.Collections.Generic;
    
    public partial class DbStudentsInGroup
    {
        public int StudentsInGroupsId { get; set; }
        public Nullable<int> GroupId { get; set; }
        public Nullable<int> StudentId { get; set; }
    
        public virtual DbGroup Group { get; set; }
        public virtual DbStudent Student { get; set; }
    }
}
