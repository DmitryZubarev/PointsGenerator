using Domain.Interfaces.Kafka;
using Infrastructure.Kafka;
using Domain.Models;
using Domain.Options;
using Infrastructure.Kafka.Producers;
using PointsGenerator;


var builder = Host.CreateApplicationBuilder(args);

//Добавление НЕстандартных источников конфигурации. Можно вынести в методы расширения
builder.Configuration.AddJsonFile("KafkaSettings.json", optional: true).AddEnvironmentVariables();

//Регистрация options. Можно вынести в метод расширения
var configuration = builder.Configuration;
builder.Services.Configure<KafkaOptions>(configuration.GetSection("KafkaConfig"));
builder.Services.Configure<ProducerOptions>(configuration.GetSection("KafkaConfig:ProducerConfig"));

//Регистрация сервисов. Нужно добавить продюсер. Можно вынести в методы расширения
builder.Services.AddHostedService<Worker>();
builder.Services.AddKafka();

var host = builder.Build();
host.Run();
