def calculate_frame_value_for_port_config(port_ctrl_enable: list[int], set_ports_to_overpress: list[int],port_status: int = 0,reverse_port: bool = False, reverse_enable: bool = False) -> list[int]:
	"""
	"""

	val_ref = 1
	list_values = [0,0]

	#reverse port and enable section 
	if reverse_port:
		temp = port_status >> 16
		new_data_overpress = temp
		new_data_enbl = temp
		for i in range(len(set_ports_to_overpress)):
			val = set_ports_to_overpress[i]%21
			new_data_overpress = (val_ref << val + 5) ^ temp
			temp=new_data_overpress

		for i in range(len(port_ctrl_enable)):
			val = port_ctrl_enable[i]%21
			new_data_enbl = (val_ref << val + 5) ^ temp
			temp=new_data_enbl

		[list_values.append((new_data_overpress >> val) & 255) for val in range(0,32,8)]
		list_values.extend([0, 0, 0, 0])
		[list_values.append((new_data_enbl >> val) & 255) for val in range(0,32,8)]
		list_values.extend([0, 0, 0, 0, 0, 0])
		return list_values

	#set port and enable 
	for attempt in range(2):
		new_data = 0
		temp = port_status

		if attempt == 0:
			length = len(set_ports_to_overpress)
			low = any(val < 20 for val in set_ports_to_overpress)
			high = any(val > 41 for val in set_ports_to_overpress)
		else:
			length = len(port_ctrl_enable)
			low = any(val < 20 for val in port_ctrl_enable)
			high = any(val > 41 for val in port_ctrl_enable)

		if low or high:
			raise ValueError(f"Unexpected port is set.Please enter valid port" )

		for i in range(length):
			if attempt == 0:
				val = set_ports_to_overpress[i]%21
			else:
				val = port_ctrl_enable[i] % 21
			new_data = (val_ref<< val+ 5)
			new_data = new_data | temp
			temp = new_data

		for i in range(0,32,8):
			list_values.append((new_data >> i) & 255)

		if attempt == 0:
			list_values.extend([0, 0, 0, 0])
		else:
			list_values.extend([0, 0, 0, 0, 0, 0 ])

	return list_values


def enabled_ports(can_response_value: int, can_response_length: int) -> list[int]:
	result = []
	for i in range(can_response_length*8):
		if can_response_value & (1<<i):
			result.append(i)
	return result