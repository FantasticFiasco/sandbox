import { Action } from './action';

export interface AddMessageAction extends Action {
  message: string;
}
