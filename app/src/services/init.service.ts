import { zzz } from "../core/time-stuff"

interface ClientInfo {
	name: string,
	ip: string
}

type CiResponse = ClientInfo | 'retry' | 'giveup'

class InitService {
	async initClient(onReady: (ci: ClientInfo) => void) {
		
		let clientInfo = await this.getClientInfo();
		
		while (clientInfo == 'retry') {						
			clientInfo = await this.getClientInfo();
			await zzz()
		}
		
		if (clientInfo == 'giveup') {
			console.error('backend panic')
			//TODO: panic
		} else {			
			onReady(clientInfo)
		}
	}	

	private async getClientInfo(): Promise<CiResponse> {
		let response = await fetch('http://localhost:5169/client-info')

		if (response.status != 200) {
			return 'retry'
		}		
				
		try {
			let text = await response.text()
			let ci: ClientInfo = JSON.parse(text)
			
			return ci;
		} catch {
			return 'giveup'
		}
		
	}

}

export type { ClientInfo }
export { InitService }

