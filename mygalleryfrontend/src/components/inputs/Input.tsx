import React from 'react'
import type { UseFormRegister } from 'react-hook-form';

type Props = {
  register: UseFormRegister<any>;
  title: string;
  name: string;
  type: string;
}

export default function Input({register, title, name, type} : Props) {
  return (
    <div>
      <label htmlFor={name}>{title}</label>
      <input type={type} {...register(name)} />
    </div>
  )
}
