using Domain.Options;
using PointsGenerator;


var builder = Host.CreateApplicationBuilder(args);

//Добавление НЕстандартных источников конфигурации. Можно вынести в методы расширения
builder.Configuration.AddJsonFile("kafkaSettings.json");

//Регистрация options. Можно вынести в метод расширения
var configuration = builder.Configuration;
builder.Services.Configure<KafkaOptions>(configuration.GetSection("KafkaConfig"));
builder.Services.Configure<ProducerOptions>(configuration.GetSection("KafkaConfig:ProducerConfig"));

//Регистрация сервисов. Нужно добавить продюсер. Можно вынести в методы расширения
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
