using Peer2Pa;
using Peer2Pa.Services;
using Peer2Pa.Ws;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton(f => new Knowledge());

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSignalR();
//builder.Services.AddHostedService<BroadcastService>();

var app = builder.Build();

if (app.Environment.IsDevelopment()) {
	app.UseSwagger();
	app.UseSwaggerUI();	
}

app.MapGet("/client-info", (Knowledge k) => k.ClientInfo);

app.MapHub<MainHub>("/p2pa");

app.Run();

