namespace ApiControleEstoque.Models.Response
{
    public class ResponseModelPadrao<T>
    {
        public bool IsSucces { get; set; }

        public string Status { get; set; }

        public List<T> Data { get; set; }
    }
}
