using System;

namespace StockTwitService.Models
{
    public class StockTwitResponse
    {
        public Response response { get; set; }
        public Error[] errors { get; set; }
        public Symbol symbol { get; set; }
        public Message[] messages { get; set; }
    }

    public class Response
    {
        public int status { get; set; }
        
    }

    public class Error
    {
        public string message { get; set; }
    }

    public class Symbol
    {
        public int id { get; set; }

        public string symbol { get; set; }

        public string title { get; set; }
        public bool is_following { get; set; }
    }

    public class Message
    {
        public int id { get; set; }
        public string body { get; set; }
        public DateTime created_At { get; set; }
    }
}