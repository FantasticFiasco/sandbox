namespace Loggers {
	export interface ILogger {
		log: (msg: string) => void;
	}

	export class Logger implements ILogger {
		log(msg: string) {
			console.log(msg);
		}
	}
}