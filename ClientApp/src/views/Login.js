import { useContext, useState } from "react"
import { UserContext } from "../context/UserProvider"
import Swal from 'sweetalert2'
import { Navigate } from "react-router-dom"

const mostrarError = (mensaje) => {
    Swal.fire({
        title: 'Error',
        text: mensaje,
        icon: 'error'
    })
}

const Login = () => {

    const [_correo, set_Correo] = useState("")
    const [_clave, set_Clave] = useState("")
    const { user, iniciarSession } = useContext(UserContext)

    if (user != null) {
        return <Navigate to="/"/>
    }

    const handleSubmit = (event) => {
        event.preventDefault();

        if (!_correo.trim() || !_clave.trim()) {
            mostrarError('Ingrese correo y contraseña');
            return;
        }

        let request = {
            correo: _correo.trim(),
            clave:_clave
        }

        fetch("api/session/Login", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json;charset=utf-8'
            },
            body: JSON.stringify(request)
        })
        .then(async (response) => {
            const data = await response.json().catch(() => ({}));
            if (!response.ok) {
                throw new Error(data?.message || 'Credenciales inválidas');
            }
            return data;
        })
        .then((dataJson) => {
            if (dataJson.idUsuario === 0) {
                mostrarError('No se encontró el usuario');
            } else {
                iniciarSession(dataJson)
            }

        }).catch((error) => {
            mostrarError(error.message || 'No se pudo iniciar sesión');
        })
    }

    return (
        <div className="container">

            <div className="row justify-content-center">

                <div className="col-xl-10 col-lg-12 col-md-9">

                    <div className="card o-hidden border-0 shadow-lg my-5">
                        <div className="card-body p-0">

                            <div className="row">
                                <div className="col-lg-6 d-none d-lg-block bg-login-image"></div>
                                <div className="col-lg-6">
                                    <div className="p-5">
                                        <div className="text-center">
                                            <h1 className="h4 text-gray-900 mb-4">Bienvenido</h1>
                                        </div>
                                        <form className="user" onSubmit={handleSubmit}>
                                            <div className="form-group">
                                                <input type="email" className="form-control form-control-user" aria-describedby="emailHelp" placeholder="Correo"
                                                    value={_correo}
                                                    onChange={(e) => set_Correo(e.target.value)}
                                                />
                                            </div>
                                            <div className="form-group">
                                                <input type="password" className="form-control form-control-user" placeholder="Contraseña"
                                                    value={_clave}
                                                    onChange={(e) => set_Clave(e.target.value)}
                                                />
                                            </div>
                                            <button type="submit" className="btn btn-primary btn-user btn-block"> Ingresar </button>
                                            
                                        </form>
                                        <hr></hr>
                                    </div>
                                </div>
                            </div>
                        </div>
                </div>

                </div>

            </div>

        </div>
        )
}

export default Login