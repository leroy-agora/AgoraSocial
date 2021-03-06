interface SendProps {
  method:'GET' | 'POST' | 'DELETE' | 'PUT' ;
  path: string;
  data?: any;
  token?: string
}

const agoraApi = async ({ method = 'GET', path, data, token }: SendProps) => {

  const headers = new Headers();

  if (token) {
    headers.append('Authorization', `Bearer ${token}`);
  }

  try {
    const res = await fetch(path, { headers });
    const data = await res.json();

    return data;
  } catch (e) {
    return e;
  }
};

export const get = async (path: string, token?: string) =>
  agoraApi({ method: 'GET', path, token });
