<script lang="ts">
	import { HubConnectionBuilder } from "@microsoft/signalr";
	import { InitService, type ClientInfo } from "./services/init.service";
	import NetworkView from "./views/NetworkView.svelte";

	const init = new InitService();

	let clientInfo: ClientInfo;
let a = 'a'
	init.initClient((client) => (clientInfo = client));

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
	
</script>

<main>
	{#if clientInfo}
		<NetworkView />

		<div class="b-panel">
			<div>{clientInfo.name}</div>
			<div>{clientInfo.ip}</div>
		</div>
	{:else}
		<div class="preload">
			<span data-uk-spinner="ratio: 5"></span>
			<h3>Inicializando</h3>
		</div>
	{/if}
</main>

<style>
	main {
		margin: 0;
	}

	.preload {
		display: flex;		
		flex-direction: column;
		height: 100vh;
		justify-content: center;
		align-items: center;
	}

	.b-panel {
		position: fixed;
		width: 100%;
		height: 40px;
		bottom: 0;
		display: flex;
		flex-direction: row;
		background-color: #333;
		color: #f2f2f2;
		align-items: center;
	}

	.b-panel div {
		flex: 1;
	}

	.b-panel div:first-child {
		margin-left: 10px;
	}

	.b-panel div:last-child {
		text-align: right;
		margin-right: 10px;
	}
</style>
