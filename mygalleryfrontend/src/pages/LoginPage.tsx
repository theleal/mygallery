import React from "react";
import Input from "../components/inputs/Input";
import { useForm } from "react-hook-form";
import type { SubmitHandler } from "react-hook-form";
import { login, type LoginPayload } from "../services/authService";



export default function LoginPage() {
  const { register, handleSubmit } = useForm<LoginPayload>();

  const handleLogin: SubmitHandler<LoginPayload> = async (data) => {
  try {
    await login(data);
    // redirecione ou avise sucesso
  } catch (err) {
    console.error(err);
    // avise o usuário
  }
}

  return (
    <div className="page">
      <form onSubmit={handleSubmit(handleLogin)}>
        <Input register={register} title="login" name="login" type="text" />
        <Input register={register} title="password" name="password" type="password" />
        <button type="submit">Entrar</button>
      </form>
    </div>
  );
}
