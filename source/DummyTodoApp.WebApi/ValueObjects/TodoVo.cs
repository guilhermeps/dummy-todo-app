﻿using Newtonsoft.Json;

namespace DummyTodoApp.WebApi.ValueObjects
{
    public class TodoVo
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("done")]
        public bool Done { get; set; }
    }
}
