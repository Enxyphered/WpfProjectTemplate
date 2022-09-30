using System;
using System.Linq;
using Newtonsoft.Json;
using System.Reflection;
using System.ComponentModel;
using System.CodeDom.Compiler;

namespace WpfProjectTemplate.Primitives;

public class ViewModelSerializer : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        if (typeof(INotifyPropertyChanged).IsAssignableFrom(objectType))
            return true;
        return false;
    }

    public override bool CanRead => false;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        => throw new InvalidOperationException("This converter does not support reading json");

    public override void WriteJson(JsonWriter writer, object obj, JsonSerializer serializer)
    {
        if (obj is null)
        {
            serializer.Serialize(writer, null);
            return;
        }

        var type = obj.GetType();
        var properties = type.GetProperties();

        writer.WriteStartObject();

        foreach (var property in properties)
        {
            if (property is null)
                continue;

            if (property
                .GetCustomAttributes<GeneratedCodeAttribute>(false)
                .Where(attrib => attrib.Tool != "CommunityToolkit.Mvvm.SourceGenerators.ObservablePropertyGenerator")
                .Any())
            {
                continue;
            }

            if (!property.CanWrite)
                continue;

            var name = property.Name;
            var value = property.GetValue(obj);

            writer.WritePropertyName(name);
            serializer.Serialize(writer, value, null);
        }

        writer.WriteEndObject();
    }
}
