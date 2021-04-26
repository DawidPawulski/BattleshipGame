export const get = (path) => {
    return fetch(process.env.REACT_APP_API + path)
        .then((response) => response.json())
        .catch((error) => {
            alert('Failed to get')
        });
}

export const create = (path, body) =>{
    return fetch(process.env.REACT_APP_API + path, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: body
    })
        .then((response) => response.json())
        .catch((error) => {
            alert('Failed to create')
        });
}

export const update = (path, body={}) =>{
    return fetch(process.env.REACT_APP_API + path, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: body
    })
        .then((response) => response.json())
        .catch((error) => {
            alert('Failed to update')
        });
}