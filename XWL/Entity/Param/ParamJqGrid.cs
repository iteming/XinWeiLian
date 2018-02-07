namespace Entity.Param
{
    /// <summary>
    /// _search=false&nd=1512731833438&rows=20&page=1&sidx=&sord=asc
    /// </summary>
    public class ParamJqGrid
    {
        public bool _search { get; set; }
        public string nd { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public string keywords { get; set; }
    }
}
