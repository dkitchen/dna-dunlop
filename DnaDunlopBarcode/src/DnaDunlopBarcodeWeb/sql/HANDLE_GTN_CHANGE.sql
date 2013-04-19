--------------------------------------------------------
--  DDL for Procedure HANDLE_GTN_CHANGE
--------------------------------------------------------
set define off;

  CREATE OR REPLACE PROCEDURE "ORACLE"."HANDLE_GTN_CHANGE" 
(p_event_log_id IN NUMBER,
p_goodyear_serial_number IN part.goodyear_serial_number%TYPE,
p_new_green_tire_number IN part.green_tire_number%TYPE)
is

  --holds old part info
  v_part_id part.part_id%TYPE;
  v_serial_number part.serial_number%type;
  v_green_tire_number part.green_tire_number%TYPE;
  
begin
    
  select PART_ID, GREEN_TIRE_NUMBER, serial_number
  into v_part_id, v_green_tire_number, v_serial_number
  from part
  where GOODYEAR_SERIAL_NUMBER = p_goodyear_serial_number;
    
  UPDATE PART
  SET GREEN_TIRE_NUMBER = p_new_green_tire_number
  ,SERIAL_NUMBER = CONCAT(p_new_green_tire_number, p_goodyear_serial_number)
  WHERE PART_ID = v_part_id;
    
  --store part history
  INSERT INTO PART_HISTORY (PART_ID, SERIAL_NUMBER, GOODYEAR_SERIAL_NUMBER, GREEN_TIRE_NUMBER)
  VALUES (v_part_id, v_serial_number, p_goodyear_serial_number, v_green_tire_number);
    
  --add a note to this event indicating old GTN
  INSERT_EVENT_LOG_DATA( p_event_log_id, 'OldGreenTireNumber', v_green_tire_number);
      
end;

/
