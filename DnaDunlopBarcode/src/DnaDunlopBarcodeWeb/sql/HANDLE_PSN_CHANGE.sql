--------------------------------------------------------
--  DDL for Procedure HANDLE_PSN_CHANGE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "ORACLE"."HANDLE_PSN_CHANGE" 
(p_event_log_id IN NUMBER,
p_goodyear_serial_number IN part.goodyear_serial_number%TYPE,
p_new_goodyear_serial_number IN part.goodyear_serial_number%TYPE)
is

  v_part_id part.part_id%TYPE;
  v_green_tire_number part.green_tire_number%type;
  v_serial_number part.serial_number%type;
  
begin
    
  select PART_ID, green_tire_number, serial_number
  into v_part_id, v_green_tire_number, v_serial_number
  from part
  where GOODYEAR_SERIAL_NUMBER = p_goodyear_serial_number;
    
  UPDATE PART
  SET GOODYEAR_SERIAL_NUMBER = p_new_goodyear_serial_number
  ,SERIAL_NUMBER = CONCAT(v_green_tire_number, p_new_goodyear_serial_number)
  WHERE PART_ID = v_part_id;
  
    --store part history
  INSERT INTO PART_HISTORY (PART_ID, SERIAL_NUMBER, GOODYEAR_SERIAL_NUMBER, GREEN_TIRE_NUMBER)
  VALUES (v_part_id, v_serial_number, p_goodyear_serial_number, v_green_tire_number);

      
  INSERT_EVENT_LOG_DATA( p_event_log_id, 'OldGoodyearSerialNumber', p_goodyear_serial_number);
      
end;

/
