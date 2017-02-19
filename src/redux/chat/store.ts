import { Action } from './action';
import { ListenerCallback } from './listener-callback';
import { Reducer } from './reducer';
import { UnsubscribeCallback } from './unsubscribe-callback';

export class Store<T> {

  private state: T;
  private listeners: ListenerCallback[] = [];

  constructor(
    private reducer: Reducer<T>,
    initialState: T) {
    this.state = initialState;
  }

  getState(): T {
    return this.state;
  }

  dispatch(action: Action): void {
    this.state = this.reducer(this.state, action);
    this.listeners.forEach((listener: ListenerCallback) => listener());
  }

  subscribe(listener: ListenerCallback): UnsubscribeCallback {
    this.listeners.push(listener);

    return () => { // returns an "unsubscribe" function
      this.listeners = this.listeners.filter(l => l !== listener);
    };
  }
}
