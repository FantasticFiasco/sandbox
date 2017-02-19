import { Action } from './action';

export interface DeleteMessageAction extends Action {
  index: number;
}
