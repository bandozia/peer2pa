<script lang="ts">
	import { HubConnectionBuilder } from "@microsoft/signalr";
	
	let conn = new HubConnectionBuilder()
		.withUrl("http://localhost:5169/p2pa")
		.build();

	conn.start()
		.then(() => {
			console.info("conectado");
		})
		.catch((e) => {
			console.error("deu ruim", e);
		});

	conn.on("Pong", (msg: string) => {
		console.info("received back: ", msg);
	});

	const testWs = () => {
		conn.invoke("Ping", "ping").catch((err) => {
			console.error("deu ruim", err);
		});
	};

	const testFetch = () => {
		fetch("http://localhost:5169/test").then((res) => {
			res.text().then((data) => {
				console.log(data);
				alert("working");
			});
		});
	};
</script>

<main>
	<button on:click={testFetch}>test fetch</button>
	<button on:click={testWs}>test ws</button>
</main>

<style>
	main {
		text-align: center;
		padding: 1em;
		max-width: 240px;
		margin: 0 auto;
	}
	
	@media (min-width: 640px) {
		main {
			max-width: none;
		}
	}
</style>
