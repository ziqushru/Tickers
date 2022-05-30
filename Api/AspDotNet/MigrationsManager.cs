using System;
using System.Threading;
using Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;

namespace AspDotNet;

public static class MigrationManager
{
	private static int number_of_retries;

	public static IHost migrateDatabase(this IHost host)
	{
		using var scope = host.Services.CreateScope();
		using var app_context = scope.ServiceProvider.GetRequiredService<Context>();

		try
		{
			app_context.Database.Migrate();
		}
		catch (SqlException e)
		{
			if (number_of_retries >= 6) throw;
			Thread.Sleep(10000);

			number_of_retries++;
			Console.WriteLine($"The server was not found or was not accessible. Retrying... #{number_of_retries}");

			Console.WriteLine(e);
			
			migrateDatabase(host);

			throw;
		}
		return host;
	}
}
