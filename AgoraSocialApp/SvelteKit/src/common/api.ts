import { token } from '$stores/session';

interface SendProps {
  method:'GET' | 'POST' | 'DELETE' | 'PUT' ;
  path: string;
  data?: any;
}

let bearerToken;

token.subscribe(tk => bearerToken = tk);

const send = async ({ method, path, data }: SendProps) => {

  const headers = new Headers();

  if (token) {
    headers.append('Authorization', `Bearer ${bearerToken}`);
  }

  try {
    const res = await fetch(path, { headers });
    const data = await res.json();

    return data;
  } catch (e) {
    return e;
  }
};

export const get = async (path: string) => send({ method: 'GET', path });
